﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The WorkspaceLocatorService class provides a service to locate a Workspace given a smartpart and
// a WorkItem.
// 
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Library.Services
{
    public class WorkspaceLocatorService : IWorkspaceLocatorService
    {
        public IWorkspace FindContainingWorkspace(WorkItem workItem, object smartPart)
        {
            while (workItem != null)
            {
                foreach (KeyValuePair<string, IWorkspace> namedWorkspace in workItem.Workspaces)
                {
                    if (namedWorkspace.Value.SmartParts.Contains(smartPart))
                        return namedWorkspace.Value;
                }
                workItem = workItem.Parent;
            }
            return null;
        }
    }
}
