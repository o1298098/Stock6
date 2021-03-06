﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.apiHelper
{
    public static class InvokeHelper
    {
        /// <summary>
        /// 登陆
        /// </summary>
        public static string Login()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(App.Context.ServerUrl, "Kingdee.BOS.WebApi.ServicesStub.AuthService.ValidateUser.common.kdsvc");
            List<object> Parameters = new List<object>();
            Parameters.Add(App.Context.DataCenterId);
            Parameters.Add("kingdee");//用户名
            Parameters.Add("kd!123456");//密码
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
            httpClient.Url = string.Concat(App.Context.ServerUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save.common.kdsvc");

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
            httpClient.Url = string.Concat(App.Context.ServerUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.ExecuteBillQuery.common.kdsvc");

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
            httpClient.Url = string.Concat(App.Context.ServerUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete.common.kdsvc");

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
            httpClient.Url = string.Concat(App.Context.ServerUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit.common.kdsvc");

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
            httpClient.Url = string.Concat(App.Context.ServerUrl, key, ".common.kdsvc");

            httpClient.Content = JsonConvert.SerializeObject(args);
            return httpClient.SysncRequest();
        }
    }
}
