using Dapper;
using MachinTest_Backend.Model;
using MachinTest_Backend.Model.DTO;
using MachinTest_Backend.Service.Interfaces;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using System.Net;
using System.Numerics;

namespace MachinTest_Backend.Service
{
    public class LocationService: ILocationService
    {
        private readonly string _ConnectionString;

        //Constructor Used for getting connection string from appsetting.json
        public LocationService(IConfiguration configuration)
        {
            this._ConnectionString = configuration["ConnectionString:DefaultConnection"].ToString();
        }
        //Method to get all locations
        public async Task<IEnumerable<Location>> GetLoactions()
        {
            try
            {
                //Establish a new connection to the database using connection string
                using(var connection=new SqlConnection(_ConnectionString))
                {
                    // If the connection is closed, open it asynchronously.
                    if (connection.State == ConnectionState.Closed)
                       await connection.OpenAsync();
                    //Execute query
                    var data = await connection.QueryAsync<Location>("SELECT CONVERT(nvarchar(36), Id) AS Id,Name, Address,Phone,Latitude,Longitude,Company FROM Locations;");
                    return data;
                }
            }catch (Exception ex)
            {
                throw;
            }
        }
        //Method to add new locations
        public async Task<bool> AddLoaction(LocationDto details)
        {
            try
            {
                //Establish a new connection to the database using connection string
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    // If the connection is closed, open it asynchronously.
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();
                    //Execute the query
                    var result = await connection.ExecuteAsync("insert into Locations (Name,Address,Phone,Latitude,Longitude,Company) values(@Name,@Address,@Phone,@Latitude,@Longitude,@Company)",
                        new { 
                            Name = details.Name, 
                            Address = details.Address, 
                            Phone = details.Phone, 
                            Latitude = details.Latitude, 
                            Longitude = details.Longitude, 
                            Company = details.Company });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Method to delete Existing Location
        public async Task<bool> DeletLoaction(string Phone)
        {
            try
            {
                //Establish a new connection to the database using connection string
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    // If the connection is closed, open it asynchronously.
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();
                    //Execute the query
                    var result = await connection.ExecuteAsync("delete from Locations where Phone = @Phone", new { Phone=Phone });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateLoaction(LocationDto location)
        {
            try
            {
                //Establish a new connection to the database using connection string
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    // If the connection is closed, open it asynchronously.
                    if (connection.State == ConnectionState.Closed)
                        await connection.OpenAsync();
                    //Execute the query
                    var result = await connection.ExecuteAsync("update Locations set Name=@Name,Address=@Address,Phone=@Phone,Latitude=@Latitude,Longitude=@Longitude,Company=@Company where Phone = @Phone",
                        new {
                            Name = location.Name,
                            Address=location.Address, 
                            Company=location.Company, 
                            Longitude=location.Longitude, 
                            Latitude=location.Latitude, 
                            Phone=location.Phone});
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
