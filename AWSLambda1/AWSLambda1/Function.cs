using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using AWSLambda1.ConfigureEmail;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        
       /// <summary>
       /// 
       /// </summary>
        public async void FunctionHandler()
        {
            DbOperations dboperation = new DbOperations();

            await dboperation.GetRDSConnectionandSendMail();
        }
    }
}
