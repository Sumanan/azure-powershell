﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.DataLakeStore.Models;

namespace Microsoft.Azure.Commands.DataLakeStore
{
    [Cmdlet(VerbsCommon.Get, "AzureRmDataLakeStoreItemOwner"), OutputType(typeof (string))]
    public class GetAzureDataLakeStoreItemOwner : DataLakeStoreFileSystemCmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Position = 0, Mandatory = true,
            HelpMessage = "The DataLakeStore account to execute the filesystem operation in")]
        [ValidateNotNullOrEmpty]
        [Alias("AccountName")]
        public string Account { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Position = 1, Mandatory = true,
            HelpMessage =
                "The path in the specified Data Lake account that should have its owner or owning group retrieved. Can be a file or folder " +
                "In the format '/folder/file.txt', " +
                "where the first '/' after the DNS indicates the root of the file system.")]
        [ValidateNotNull]
        public DataLakeStorePathInstance Path { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Position = 2, Mandatory = true,
            HelpMessage = "The type of owner to get. Valid values are 'user' and 'group'.")]
        [ValidateNotNull]
        public DataLakeStoreEnums.Owner Type { get; set; }

        protected override void ProcessRecord()
        {
            var aclObject = DataLakeStoreFileSystemClient.GetAclStatus(Path.TransformedPath, Account);
            WriteObject(Type == DataLakeStoreEnums.Owner.Group ? aclObject.Group : aclObject.Owner);
        }
    }
}