﻿using System;
using System.Collections.Generic;
using CommonInterfaces;

namespace MQTTCommunicatorDummy
{
   
    public class MQTTCommunicator : IMQTTCommunicator
    {
        static void Main(string[] args)
        {
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

        //void IMQTTCommunicator.ConnectToBroker(dynamic Host, int Port)
       // {
          //  throw new NotImplementedException();
       // }


        string IMQTTCommunicator.ConnectToBroker(string Host, int Port, string Username, string Password)
        {
            throw new NotImplementedException();
        }

        void IMQTTCommunicator.CreateTopic(string topicName)
        {
            throw new NotImplementedException();
        }

        void IMQTTCommunicator.PublishToTopic(string topicName, string messagePayload)
        {
            throw new NotImplementedException();
        }


        void IMQTTCommunicator.SetNewBroker(string Host, int Port, string Username, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
