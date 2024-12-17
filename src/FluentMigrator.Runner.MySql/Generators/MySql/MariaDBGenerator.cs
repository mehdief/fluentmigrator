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

using JetBrains.Annotations;

using Microsoft.Extensions.Options;

namespace FluentMigrator.Runner.Generators.MySql
{
    public class MariaDBGenerator : MySql8Generator
    {
        public MariaDBGenerator()
            : this(new MariaDBQuoter())
        {
        }

        public MariaDBGenerator(
            [NotNull] MariaDBQuoter quoter)
            : this(quoter, new OptionsWrapper<GeneratorOptions>(new GeneratorOptions()))
        {
        }

        public MariaDBGenerator(
            [NotNull] MariaDBQuoter quoter,
            [NotNull] IMariaDBTypeMap typeMap)
            : this(quoter, typeMap, new OptionsWrapper<GeneratorOptions>(new GeneratorOptions()))
        {
        }

        public MariaDBGenerator(
            [NotNull] MariaDBQuoter quoter,
            [NotNull] IOptions<GeneratorOptions> generatorOptions)
            : this(
                new MariaDBColumn(new MariaDBTypeMap(), quoter),
                quoter,
                new EmptyDescriptionGenerator(),
                generatorOptions)
        {
        }

        public MariaDBGenerator(
            [NotNull] MariaDBQuoter quoter,
            [NotNull] IMariaDBTypeMap typeMap,
            [NotNull] IOptions<GeneratorOptions> generatorOptions)
            : base(new MariaDBColumn(typeMap, quoter), quoter, new EmptyDescriptionGenerator(), generatorOptions)
        {
        }

        protected MariaDBGenerator(
            [NotNull] IColumn column,
            [NotNull] IQuoter quoter,
            [NotNull] IDescriptionGenerator descriptionGenerator,
            [NotNull] IOptions<GeneratorOptions> generatorOptions)
            : base(column, quoter, descriptionGenerator, generatorOptions)
        {
        }
    }
}
