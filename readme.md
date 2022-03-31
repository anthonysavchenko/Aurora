# Система учета расчетов с абонентами ЖКХ


## Настройка среды разработки


1. Установить Visual Studio 2022 или выше.

1. Для использования фрэймворка 4.0 нужно скачать его с nuget `https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies.net40/`, переименовать с расширением .zip, разархивировать, скопировать данные из папки `build\.NETFramework\v4.0` в `C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0`.

1. Установить DevExpress 13.1.

1. Клонировать репозиторий. Открыть и отстроить код (сначала в конфигурации Release, потом Debug). Иначе могут быть проблемы с отсутствием файлов `Model.csdl`, `Model.msl` и `Model.ssdl`.

1. Ошибки вида `Importing key file 'SOME_FILE_NAME.pfx was canceled. Cannot import ... container name: VS_KEY_SOME_THING` исправить, используя команду вида `C:\"Program Files (x86)"\"Microsoft SDKs"\Windows\v10.0A\bin\"NETFX 4.8 Tools"\sn.exe -i SOME_FILE_NAME.pfx VS_KEY_SOME_THING` (https://stackoverflow.com/questions/2815366/cannot-import-the-keyfile-blah-pfx-error-the-keyfile-may-be-password-protec).
