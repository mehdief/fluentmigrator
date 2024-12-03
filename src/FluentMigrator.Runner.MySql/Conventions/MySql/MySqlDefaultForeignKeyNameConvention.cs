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

using System.Text;

using FluentMigrator.Expressions;
using FluentMigrator.Model;

namespace FluentMigrator.Runner.Conventions.MySql
{
    public class MySqlDefaultForeignKeyNameConvention : IForeignKeyConvention
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
}
