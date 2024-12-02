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
using System.Text;

using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Conventions;

namespace FluentMigrator.Runner.ConventionSets
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
                new MyPrimaryKeyNameConvention()
            };

            ConstraintConventions = new List<IConstraintConvention>
            {
                new MyConstraintNameConvention(),
                schemaConvention,
            };

            ForeignKeyConventions = new List<IForeignKeyConvention>
            {
                new MyForeignKeyNameConvention(),
                schemaConvention,
            };

            IndexConventions = new List<IIndexConvention>
            {
                new MyIndexNameConvention(),
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

    class MyPrimaryKeyNameConvention : IColumnsConvention
    {
        public IColumnsExpression Apply(IColumnsExpression expression)
        {
            foreach (var columnDefinition in expression.Columns)
            {
                if (columnDefinition.IsPrimaryKey && string.IsNullOrEmpty(columnDefinition.PrimaryKeyName))
                {
                    columnDefinition.PrimaryKeyName = GetPrimaryKeyName();
                }
            }

            return expression;
        }

        private static string GetPrimaryKeyName()
        {
            return "PRIMARY";
        }
    }

    class MyConstraintNameConvention : IConstraintConvention
    {
        public IConstraintExpression Apply(IConstraintExpression expression)
        {
            if (string.IsNullOrEmpty(expression.Constraint.ConstraintName))
            {
                expression.Constraint.ConstraintName = GetConstraintName(expression.Constraint);
            }

            return expression;
        }

        private static string GetConstraintName(ConstraintDefinition expression)
        {
            if (expression.IsPrimaryKeyConstraint) return "PRIMARY";

            var builder = new StringBuilder();
            builder.Append(expression.TableName);
            foreach (var column in expression.Columns)
            {
                builder.Append($"_{column}");
            }

            builder.Append("_unique");
            return builder.ToString();
        }
    }

    class MyForeignKeyNameConvention : IForeignKeyConvention
    {
        public IForeignKeyExpression Apply(IForeignKeyExpression expression)
        {
            if (string.IsNullOrEmpty(expression.ForeignKey.Name))
            {
                expression.ForeignKey.Name = GetForeignKeyName(expression.ForeignKey);
            }

            return expression;
        }

        private static string GetForeignKeyName(ForeignKeyDefinition foreignKey)
        {
            var builder = new StringBuilder();

            builder.Append(foreignKey.ForeignTable);

            foreach (var foreignColumn in foreignKey.ForeignColumns)
            {
                builder.Append($"_{foreignColumn}");
            }

            builder.Append('_');
            builder.Append(foreignKey.PrimaryTable);

            foreach (var primaryColumn in foreignKey.PrimaryColumns)
            {
                builder.Append($"_{primaryColumn}");
            }

            builder.Append("_foreign");

            return builder.ToString();
        }
    }

    class MyIndexNameConvention : IIndexConvention
    {
        public IIndexExpression Apply(IIndexExpression expression)
        {
            if (string.IsNullOrEmpty(expression.Index.Name))
            {
                expression.Index.Name = GetIndexName(expression.Index);
            }

            return expression;
        }

        private static string GetIndexName(IndexDefinition index)
        {
            var builder = new StringBuilder();

            builder.Append(index.TableName);

            foreach (var column in index.Columns)
            {
                builder.Append($"_{column.Name}");
            }

            builder.Append("_index");

            return builder.ToString();
        }
    }
}
