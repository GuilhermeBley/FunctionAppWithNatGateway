using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using System.Data.Common;
using Dapper;

namespace FunctionAppWithNatGateway;

public static class Function1
{
    [FunctionName("Function1")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string sql = "SELECT * FROM YourTable";
        
        using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("MyConnectionString"));

        var dynamicObject = connection.Query(sql);

        return new OkObjectResult(dynamicObject);
    }
}
