using Taumis.Infrastructure.Interface;
using Taumis.EnterpriseLibrary.Win.Services;
using Mappers = Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers;

namespace Taumis.Infrastructure.Module
{
    public class ModuleController : WorkItemController
    {
        public override void Run()
        {
            IDataMapperService trans = WorkItem.RootWorkItem.Services.Get<IDataMapperService>();

            trans.RegisterDataMapper(new Mappers.RefBook.BuildingDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.BuildingCounterDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.UserDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.PrivateCounterDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.RouteFormValueDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.FillFormValueDataMapper());
            trans.RegisterDataMapper(new Mappers.RefBook.BuildingCounterValueDataMapper());

            trans.RegisterDataMapper(new Mappers.Doc.CustomerDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.RouteFormDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.RouteFormPosDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.FillFormDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.FillFormPosDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.DecFormsUploadDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.DecFormsUploadPosDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.AttachmentDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.DecFormsDownloadDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.EmailDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.BuildingValuesUploadDataMapper());
            trans.RegisterDataMapper(new Mappers.Doc.BuildingValuesUploadPosDataMapper());
        }
    }
}
