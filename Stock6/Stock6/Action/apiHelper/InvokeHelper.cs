﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.apiHelper
{
    public static class InvokeHelper
    {
        private static string CloudUrl = "http://192.168.1.52/k3cloud/";//K/3 Cloud 业务站点地址http://canda.f3322.net:8003/k3cloud/

        /// <summary>
        /// 登陆
        /// </summary>
        public static string Login()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.AuthService.ValidateUser.common.kdsvc");
            List<object> Parameters = new List<object>();
            Parameters.Add("5ab05fc34e03d1");//帐套Id 测试5ab05fc34e03d1 正式59a12c8ba824d2
            Parameters.Add("何志彬");//用户名
            Parameters.Add("o1298098@live.com");//密码
            Parameters.Add(2052);
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Save(string formId, string content)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save.common.kdsvc");

            List<object> Parameters = new List<object>();
            //业务对象Id 
            Parameters.Add(formId);
            //Json字串
            Parameters.Add(content);
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }
        /// <summary>
        /// 单据查询
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ExecuteBillQuery(string content)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.ExecuteBillQuery.common.kdsvc");

            List<object> Parameters = new List<object>();
            //Json字串
            Parameters.Add(content);
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Delete(string formId, string content)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete.common.kdsvc");

            List<object> Parameters = new List<object>();
            //业务对象Id 
            Parameters.Add(formId);
            //Json字串
            Parameters.Add(content);
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Audit(string formId, string content)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit.common.kdsvc");

            List<object> Parameters = new List<object>();
            //业务对象Id 
            Parameters.Add(formId);
            //Json字串
            Parameters.Add(content);
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }

        /// <summary>
        /// 自定义
        /// </summary>
        /// <param name="key">自定义方法标识</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static string AbstractWebApiBusinessService(string key, List<object> args)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(CloudUrl, key, ".common.kdsvc");

            httpClient.Content = JsonConvert.SerializeObject(args);
            return httpClient.SysncRequest();
        }
    }
}
