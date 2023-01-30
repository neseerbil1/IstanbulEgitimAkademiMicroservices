using AkademiECommerce.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AkademiECommerce.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration, IDbConnection dbConnection)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("Delete from Discount where id=@Id", new {Id=id});
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Silinecek değer bulunamadı", 404);
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
            return ResponseDto<List<Models.Discount>>.Success(discount.ToList(), 200);

        }

        public async Task<ResponseDto<Models.Discount>> GetByCodeUser(string code, string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", 
                new {Id=id})).SingleOrDefault();
            if(discount == null)
            {
                return ResponseDto<Models.Discount>.Fail("Değer bulunamadı", 404);
            }
            return ResponseDto<Models.Discount>.Success(discount, 200);
        }

        public async Task<ResponseDto<NoContent>> Insert(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Insert into discount (UserId,Rate,Code) values(@UserId,@Rate,@Code)", discount);
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Bir hata oluştu", 500);

        }

        public async Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Update Discount set userid=@UserId, rate=@Rate, code=@Code where id=@Id",
                new 
                {
                    Id=discount.ID,
                    UserId=discount.UserID,
                    Rate=discount.Rate,
                    Code=discount.Code, 
            } );
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Bir hata oluştu", 500);
        }
    }
}
