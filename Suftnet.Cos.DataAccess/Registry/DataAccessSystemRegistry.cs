namespace Suftnet.Cos.DataAccess
{
   using StructureMap.Configuration.DSL;  

   public class DataAccessSystemRegistry : Registry
   {
       public DataAccessSystemRegistry()
       {
          For<ICommon>().Use<Common>();
          For<ISettings>().Use<Settings>();
          For<IAddress>().Use<Address>();
          For<IEditor>().Use<Editor>();              
          For<IGlobal>().Use<Global>();  
          For<ITenant>().Use<Tenant>();           
          For<ILogViewer>().Use<Logger>();
          For<IMobileLogger>().Use<MobileLogger>();

          For<IPlanFeature>().Use<PlanFeature>();
          For<IPlan>().Use<Plan>();
          For<IPermission>().Use<Permission>();        
          For<ISlider>().Use<Slider>();
          For<IFaq>().Use<Faq>();
          For<ITour>().Use<Tour>();          
          For<IChapter>().Use<Chapter>();
          For<ITopic>().Use<Topic>();
          For<ISubTopic>().Use<SubTopic>();            
        }

   } 
}
