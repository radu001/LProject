using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Net;

namespace CloudBackup.Clouds
{
    class DropBoxController : ICloud
    {
        NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public object MessageBox { get; private set; }

        public DropBoxController()
        {
            //var task = Task.Run((Func<Task>)DropBox.Connect);
            //task.Wait();
        }

        public String PrepareUri()
        {
            var appKey = appSettings["dropboxAppKey"];
            var redirectUri = appSettings["dropboxRedirectUri"];
            var uri = string.Format(
                @"https://www.dropbox.com/1/oauth2/authorize?response_type=token&redirect_uri={0}&client_id={1}",
                redirectUri, appKey);
            return uri;
        }

        static async Task Connect()
        {
            using (var dbx = new DropboxClient("GS22QMqeLQ0AAAAAAAABgckLNV9clypyh2QwOcodvfo_r_om8xpG6GKcuHqrYBUi"))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }

        public string ParseUriForToken(Uri uri)
        {
            var redirectUri = appSettings["dropboxRedirectUri"];
            if (uri.AbsoluteUri.StartsWith(@redirectUri) && !uri.AbsoluteUri.Equals(@redirectUri))
            {
                var accessToken = HttpUtility.ParseQueryString(uri.Fragment.Substring(1))["access_token"];
                return accessToken;
            }
            return null;
        }
    }
}
