namespace Suftnet.Cos.Web
{
    using System.Web.Optimization;

    public class OptimizationService : IOptimizationService
    {
        public OptimizationService()
        {
        }

        public void GenerateMetasInformations(System.Web.HttpContextBase context, object model)
        {
            // Do nothing
        }

        public void RegisterBundles(System.Web.HttpContextBase context)
        {                      
            
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/confirm/jqueryconfirm").Include(
                     "~/Content/zice-OneChurch/components/confirm/jquery.confirm.css"
             ));                  
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/validationEngine/validationEngine").Include(
                    "~/Content/zice-OneChurch/components/validationEngine/validationEngine.jquery.css"
             ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/jscrollpane/jscrollpane").Include(
                    "~/Content/zice-OneChurch/components/jscrollpane/jscrollpane.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/tipsy/css/tipsy").Include(
               "~/Content/zice-OneChurch/components/tipsy/css/jquery.tipsy.css"
            ));           
           
            BundleTable.Bundles.Add(new StyleBundle("~/Content/slim/lib/font-awesome/css").Include(
                    "~/Content/slim/lib/font-awesome/css/font-awesome.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/ui/jquery-ui.css").Include(
                    "~/Content/zice-OneChurch/components/ui/jquery-ui-1.12.1/jquery-ui.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/dataTables").Include(
                    "~/Content/zice-OneChurch/components/datatables/dataTables.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/components/validationEngine/validationEngine-css").Include(
                    "~/Content/zice-OneChurch/components/validationEngine/validationEngine.jquery.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/zice-OneChurch/css/mainCss").Include(
                       "~/Content/zice-OneChurch/css/zice.style.css",
                       "~/Content/zice-OneChurch/css/icon.css",
                       "~/Content/zice-OneChurch/css/ui-custom.css",
                       "~/Content/zice-OneChurch/css/timepicker.css",
                       "~/Content/zice-OneChurch/css/buttons.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/custom").Include(                
                       "~/Content/css/form.css",
                       "~/Content/css/error.css"                       
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/bundle/perfectscrollbar").Include(
                        "~/Content/perfect-scrollbar/css/perfect-scrollbar.min.css"
            ));
            BundleTable.Bundles.Add(new StyleBundle("~/validation/bundle").Include(
                        "~/Content/css/validationSummary.css"
            ));                      
            BundleTable.Bundles.Add(new StyleBundle("~/bundle/zebra-tooltips").Include(
                        "~/Content/zebra-tooltips/css/default/zebra_tooltips.min.css"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/zice-OneChurch/components").Include(                                        
                        "~/Content/zice-OneChurch/components/scrolltop/scrolltopcontrol.js",
                        "~/Content/zice-OneChurch/components/tipsy/js/jquery.tipsy.js",
                        "~/Content/zice-OneChurch/components/fullcalendar/fullcalendar.js",            
                        "~/Content/zice-OneChurch/components/placeholder/jquery.placeholder.js",
                        "~/Content/zice-OneChurch/js/zice.custom.js"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/zice-OneChurch/adminComponents").Include(
                        "~/Content/zice-OneChurch/components/scrolltop/scrolltopcontrol.js",
                        "~/Content/zice-OneChurch/components/fullcalendar/fullcalendar.js",
                        "~/Content/zice-OneChurch/components/tipsy/js/jquery.tipsy.js",
                        "~/Content/zice-OneChurch/components/placeholder/jquery.placeholder.js"                    
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/jqueryUi").Include(               
                        "~/Content/zice-OneChurch/js/jquery.cookie.js",
                        "~/Content/zice-OneChurch/components/ui/jquery-ui-1.12.1/jquery-ui.js"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/dataTables").Include(
                        "~/Content/zice-OneChurch/components/datatables/media/js/jquery.dataTables.js"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/validationEngine").Include(
                        "~/Content/zice-OneChurch/components/jQuery-Validation-Engine-master/js/jquery.validationEngine.js",
                        "~/Content/zice-OneChurch/components/jQuery-Validation-Engine-master/js/languages/jquery.validationEngine-en.js"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/Common")
                .IncludeDirectory("~/Scripts/common", "*.js"));
          
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/jsSuftnet").Include(
                       "~/Scripts/jsSuftnet/suftnet.Http.js",
                       "~/Scripts/jsSuftnet/suftnet.tools.js"
            ));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/lib").Include(                
                       "~/Scripts/lib/modernizr-1.7.js",
                       "~/Scripts/lib/rsvp-3.1.0.min.js",                    
                       "~/Scripts/lib/jquery.loading.min.js",                 
                       "~/Scripts/lib/perfect-scrollbar.jquery.min.js"         
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/rsvp").Include(
                       "~/Scripts/lib/rsvp-3.1.0.min.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/zebra_tooltips_js").Include(                   
                       "~/Content/zebra-tooltips/zebra_tooltips.min.js"
            ));      

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/checkout").Include(
                       "~/Scripts/checkout/jquery.payment.js",
                       "~/Scripts/checkout/checkout.js"                  
           ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/trialcheckout").Include(
                       "~/Scripts/checkout/trial.checkout.js"               
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/ajax").Include(
                       "~/Scripts/ajax/suftnet.Http.js",
                       "~/Scripts/ajax/suftnet.tools.js"
            ));            

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/LoadingSpinnerSprite").Include(
                  "~/Content/Loading-Spinner-Sprite/jquery.preloaders.min.js",
                  "~/Scripts/checkout/spinner.js"
            ));                

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/slim-lib-js").Include(                      
                    "~/Content/slim/lib/popper/js/popper.js",
                    "~/Content/slim/lib/d3/js/d3.js",                   
                    "~/Content/slim/lib/jquery.maskedinput/js/jquery.maskedinput.js",
                    "~/Content/slim/lib/bootstrap/bootstrap.js"                         
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/slim-lib-jquery").Include(
                   "~/Content/slim/lib/jquery/js/jquery.js"                  
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/slim-js").Include(
                    "~/Content/slim/js/ResizeSensor.js",
                    "~/Content/slim/js/dashboard.js"
                    //"~/Content/slim/js/slim.js"
            ));

            BundleTable.Bundles.Add(new StyleBundle("~/bundle/slim-dashboard-css").Include(
                    "~/Content/slim/css/slim.dashboard.css"
            ));          

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/10000").Include(
                    "~/Scripts/10000/suftnet_upload.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/uploadifive-css").Include(
                    "~/Scripts/UploadiFive/uploadifive.css"
             ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/uploadifive").Include(
                   "~/Scripts/UploadiFive/jquery.uploadifive.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/60000").Include(
                   "~/Scripts/60000/support.js"
            ));                      
            
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/70000")
               .IncludeDirectory("~/Scripts/70000", "*.js"));           

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/zice-OneChurch/dashbaord").Include(              
                    "~/Content/zice-OneChurch/js/jquery.cookie.js",
                    "~/Content/zice-OneChurch/js/zice.dashboard.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/zice-admin/jquery").Include(         
                    "~/Content/zice-OneChurch/js/jquery.cookie.js"            
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/110000").Include(
                    "~/Scripts/60000/menu.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/multisliderjs").Include(
                    "~/Scripts/lib/multislider.js"
            ));

            BundleTable.Bundles.Add(new StyleBundle("~/bundle/multislider-css").Include(
                    "~/Content/css/multislider.css"  
            ));                                

            BundleTable.Bundles.Add(new StyleBundle("~/bundle/confirmDialog-css").Include(
                    "~/Content/confirmDialog/confirmDialog.css"
            ));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/confirmDialog-js").Include(
                    "~/Content/confirmDialog/confirmDialog.js"
            ));        
           

            BundleTable.EnableOptimizations = true;
        }        
    }    
}