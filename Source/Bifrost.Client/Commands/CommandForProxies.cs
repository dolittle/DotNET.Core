﻿#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Execution;
using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandForProxies"/>
    /// </summary>
    [Singleton]
    public class CommandForProxies : ICommandForProxies
    {
        ProxyGenerator _proxyGenerator;
        IProxying _proxying;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandForProxies"/>
        /// </summary>
        public CommandForProxies(IProxying proxying)
        {
            _proxying = proxying;
            _proxyGenerator = new ProxyGenerator();
        }


#pragma warning disable 1591 // Xml Comments
        public ICommandFor<T> GetFor<T>() where T : ICommand, new()
        {
            var command = new T();

            var interfaceForCommandType = _proxying.BuildInterfaceWithPropertiesFrom(typeof(T));

            var options = new ProxyGenerationOptions();
            var commandForInterceptor = new CommandForProxyInterceptor();

            var type = _proxyGenerator.ProxyBuilder.CreateClassProxyType(
                typeof(CommandInstanceHolder), 
                new[] { 
                    typeof(ICommandFor<T>), 
                    interfaceForCommandType, 
                    typeof(System.Windows.Input.ICommand), 
                    typeof(IHoldCommandInstance) 
                }, options);

            var i = Activator.CreateInstance(type, new[] { 
                new IInterceptor[] { 
                    commandForInterceptor,
                } 
            }) as ICommandFor<T>;

            ((IHoldCommandInstance)i).CommandInstance = command;

            return i;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
