using System.Web.Mvc;

namespace EssentialNewsMvc.Web.App_Start
{
    public class ViewEnginesConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            /////Remove All Engine
            viewEngines.Clear();
            /////Add Razor Engine
            viewEngines.Add(new RazorViewEngine());
        }
    }
}