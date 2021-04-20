using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

namespace AwsDotnetCsharp
{
    public class Handler
    {

      private readonly JsonSerializerOptions _options;

      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]
      public string FunctionTriggeredBySQS(SQSEvent sqsEvent)
      {

        Console.WriteLine($"Beginning to process {sqsEvent.Records.Count} records...");

        foreach (var record in sqsEvent.Records)
        {
          Console.WriteLine($"Message ID: {record.MessageId}");
          Console.WriteLine($"Record Body:");
          Console.WriteLine(record.Body);
        }

        Console.WriteLine("Processing complete.");

        return $"Processed {sqsEvent.Records.Count} records.";
      }

      [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]
      public APIGatewayProxyResponse SecondFunction(APIGatewayProxyRequest request)
      {
        // deserialze json input
        Request input = JsonSerializer.Deserialize<Request>(request.Body, _options);

        // just return the same to the output 
        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(input, _options)
        };
  
      }

    }

    public class Request
    {
      public string Key1 {get; set;}
      public string Key2 {get; set;}
      public string Key3 {get; set;}
    }
}
