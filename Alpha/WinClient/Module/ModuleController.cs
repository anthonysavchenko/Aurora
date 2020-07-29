using Taumis.Infrastructure.Interface;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Module
{
    public class ModuleController : WorkItemController
    {
        public override void Run()
        {
            IDataMapperService trans = WorkItem.RootWorkItem.Services.Get<IDataMapperService>();

            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.BuildingDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.UserDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.PrivateCounterDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.RouteFormValueDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook.FillFormValueDataMapper());

            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.CustomerDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RouteFormDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.RouteFormPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.FillFormDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.FillFormPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DecFormsUploadDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DecFormsUploadPosDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.AttachmentDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.DecFormsDownloadDataMapper());
            trans.RegisterDataMapper(new Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc.EmailDataMapper());
        }
    }
}