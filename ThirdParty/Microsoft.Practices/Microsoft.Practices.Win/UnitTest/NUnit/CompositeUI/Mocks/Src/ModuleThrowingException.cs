﻿//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace ModuleThrowingException
{
    public class TestModule : ModuleInit
    {
        public override void AddServices()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Load()
        {
            throw new Exception("The method or operation is not implemented.");
        }
}
}
