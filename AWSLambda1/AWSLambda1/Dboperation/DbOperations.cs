using AWSLambda1.Models;
using AWSLambda1.Util;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AWSLambda1.ConfigureEmail
{
    class DbOperations
    {
        public static IConfigurationRoot Configuration { get; set; }


        public async Task GetRDSConnectionandSendMail()
        {
            #region value fetch from appsetting.sample.json
            
            //var a = Directory.GetCurrentDirectory();
            //var builder = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.sample.json");

            //Configuration = builder.Build();

            //EmailSettingsAC emailSettings = new EmailSettingsAC
            //{
            //    From = Configuration["EmailSettings:From"],
            //    SMTPHost = Configuration["EmailSettings:SMTPHost"],
            //    SMTPPassword = Configuration["EmailSettings:SMTPPassword"],
            //    SMTPPortNo = Configuration["EmailSettings:SMTPPortNo"],
            //    SMTPUserName = Configuration["EmailSettings:SMTPUserName"],
            //    Subject = Configuration["EmailSettings:Subject"]
            //};

            #endregion

            #region  Fetch Data from Environment variable

            var connectionString = Environment.GetEnvironmentVariable("webaccountingDB");

            //Add value to EmailsettingsAc model
            EmailSettingsAC emailSettings = new EmailSettingsAC
            {
                From = Environment.GetEnvironmentVariable("Email_From"),
                SMTPHost = Environment.GetEnvironmentVariable("Email_SmtpHost"),
                SMTPPassword = Environment.GetEnvironmentVariable("Email_SmtpPassword"),
                SMTPPortNo = Environment.GetEnvironmentVariable("Email_SmtpPort"),
                SMTPUserName = Environment.GetEnvironmentVariable("Email_UserName"),
                Subject = Environment.GetEnvironmentVariable("Email_subject")
            };
            #endregion

            #region Sql query and corresponding task
            
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                List<string> emailListOfUsers = new List<string>();
                string sqlQuery = string.Format("SELECT Email FROM master_client.accountant;");

                var operatorEmail = new MySqlCommand(sqlQuery, conn);
                var promise = await operatorEmail.ExecuteReaderAsync();

                while (promise.Read())
                {
                    for (int i = 0; i < promise.FieldCount; i++)
                    {
                        emailListOfUsers.Add(promise[i].ToString());
                    }
                }
                conn.Close();
                MailOperator mailoperator = new MailOperator();
                mailoperator.sendMail(emailSettings, emailListOfUsers);

            }

            #endregion


        }
    }
}
