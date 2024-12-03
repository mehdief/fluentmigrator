﻿#region License
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
    public class MySqlDefaultConstraintNameConvention : IConstraintConvention
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
}
