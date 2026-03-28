using System;
using System.Text.Json;
using Apache.NMS;
using Core.Enums;
using Core.Response;

const string brokerUri = "amqp://localhost:5672";
const string queueName = "delivers";
IConnectionFactory factory = new NMSConnectionFactory(brokerUri);
using (var connection = factory.CreateConnection())
{
    try
    {
        connection.Start();
        using (var session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
        {
            IDestination queue = session.GetQueue(queueName);
            using (var consumer = session.CreateConsumer(queue))
            {
                var message = consumer.Receive();
                if (message is ITextMessage textMessage)
                {
                    var messageContent = textMessage.Text;
                    Console.WriteLine($"Received message: {messageContent}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        var response = Response<string>.Failure(
            EStatues.ONFAILURE,
            ex.Message
        );
        Console.WriteLine(JsonSerializer.Serialize(response));
    }
    finally
    {
        if (connection != null && connection.IsStarted)
        {
            connection.Stop();
            connection.Close();
        }
    }
}