using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daishi.AMQP;

namespace TrianglePerimeterMicroservice.Microservice
{
    public class TrianglePerimeterMicroservice : Microservice
    {
        RabbitMQAdapter _adapter;
        RabbitMQConsumerCatchAll _rabbitMqConsumerCatchAll;
        public void Init()
        {
            _adapter = RabbitMQAdapter.Instance;
            _adapter.Init("localhost", 5672, "guest", "guest", 50);

            _rabbitMqConsumerCatchAll = new RabbitMQConsumerCatchAll("TrianglePerimeter", 5000);
            _rabbitMqConsumerCatchAll.MessageReceived += OnMessageReceived;

            _adapter.Connect();
            _adapter.ConsumeAsync(_rabbitMqConsumerCatchAll);
         }

        public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string[] edges = e.Message.Split('/');
            Console.WriteLine("Received: " + e.Message);
            var result = Convert.ToInt32(edges[0]) + 
                Convert.ToInt32(edges[1]) + Convert.ToInt32(edges[2]);

            _adapter.Publish(result.ToString(), "TrianglePerimeterResult");
            Console.WriteLine("Published: " + result);
        }

        public void Shutdown()
        {
            if (_adapter == null) return;
            if(_rabbitMqConsumerCatchAll != null)
            {
                _adapter.StopConsumingAsync(_rabbitMqConsumerCatchAll);
            }
            _adapter.Disconnect();
        }
    }
}
