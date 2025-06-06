﻿using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Silmoon.AspNetCore.Interfaces;
using Silmoon.AspNetCore.SdkApiTesting.Models;
using Silmoon.AspNetCore.SdkApiTesting.Services;
using Silmoon.Data.MongoDB;
using Silmoon.Extension;
using Silmoon.Models;
using Silmoon.Secure;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Silmoon.AspNetCore.SdkApiTesting
{
    public class Core : MongoService, IDisposable
    {
        public override MongoExecuter Executer { get; set; }
        public SilmoonConfigureServiceImpl SilmoonConfigureService { get; set; }
        public ILogger<Core> Logger { get; set; }

        public Core(ISilmoonConfigureService silmoonConfigureService, ILogger<Core> logger)
        {
            Logger = logger;
            SilmoonConfigureService = (SilmoonConfigureServiceImpl)silmoonConfigureService;
            Executer = new MongoExecuter(SilmoonConfigureService.MongoDBConnectionString);
            Logger.LogInformation("当前数据库：" + Executer.Database.DatabaseNamespace.DatabaseName);
        }
        public User GetUser(string Username)
        {
            if (Username.IsNullOrEmpty()) return null;
            return new User()
            {
                Username = Username,
                Password = "123".GetMD5Hash(),
            };
        }
        public StateSet<bool> NewUser(User user)
        {
            if (user.Username.IsNullOrEmpty() || user.Password.IsNullOrEmpty()) return false.ToStateSet("Username or Password is empty.");
            return true.ToStateSet(user.Username);
        }

        public void Dispose()
        {
            Executer = null;
        }

    }
}
