﻿using System;
using System.Collections.Generic;
using CommonInterfaces;

namespace MQTTCommunicator
{
    public class MQTTCommunicator : IMQTTCommunicator
    {
        public void ConnectToBroker(dynamic Host, int Port)
        {
            throw new NotImplementedException();
        }

        public void CreateTopic(string clientId, string topicName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetClients()
        {
            throw new NotImplementedException();
        }

        public List<string> GetTopics()
        {
            throw new NotImplementedException();
        }

        public void PublishToTopic(string clientId, string topicName, dynamic value)
        {
            throw new NotImplementedException();
        }

        public void RegisterClient(string clientId, bool isGroup)
        {
            throw new NotImplementedException();
        }

        public void SetNewBroker(dynamic Host, int Port)
        {
            throw new NotImplementedException();
        }

        public void SubscribeTopic(string clientId, string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
