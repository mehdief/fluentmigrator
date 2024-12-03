#region License
// Copyright (c) 2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Collections.Generic;

namespace FluentMigrator.Runner.Conventions.MySql
{
    public class MySqlDefaultConventionSet : IConventionSet
    {
        public MySqlDefaultConventionSet()
            : this(defaultSchemaName: null, workingDirectory: null)
        {
        }

        public MySqlDefaultConventionSet(string defaultSchemaName, string workingDirectory)
        {
            var schemaConvention =
                new DefaultSchemaConvention(new DefaultSchemaNameConvention(defaultSchemaName));

            ColumnsConventions = new List<IColumnsConvention>
            {
                new MySqlDefaultPrimaryKeyNameConvention()
            };

            ConstraintConventions = new List<IConstraintConvention>
            {
                new MySqlDefaultConstraintNameConvention(),
                schemaConvention,
            };

            ForeignKeyConventions = new List<IForeignKeyConvention>
            {
                new MySqlDefaultForeignKeyNameConvention(),
                schemaConvention,
            };

            IndexConventions = new List<IIndexConvention>
            {
                new MySqlDefaultIndexNameConvention(),
                schemaConvention,
            };

            SequenceConventions = new List<ISequenceConvention>
            {
                schemaConvention,
            };

            AutoNameConventions = new List<IAutoNameConvention>
            {
                new DefaultAutoNameConvention(),
            };

            SchemaConvention = schemaConvention;
            RootPathConvention = new DefaultRootPathConvention(workingDirectory);
        }

        public IRootPathConvention RootPathConvention { get; }
        public DefaultSchemaConvention SchemaConvention { get; }
        public IList<IColumnsConvention> ColumnsConventions { get; }
        public IList<IConstraintConvention> ConstraintConventions { get; }
        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }
        public IList<IIndexConvention> IndexConventions { get; }
        public IList<ISequenceConvention> SequenceConventions { get; }
        public IList<IAutoNameConvention> AutoNameConventions { get; }
    }
}
