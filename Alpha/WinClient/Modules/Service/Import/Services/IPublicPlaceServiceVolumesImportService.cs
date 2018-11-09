namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public interface IPublicPlaceServiceVolumesImportService : IImportService
    {
        bool GenerateTemplate(string filePath);
    }
}