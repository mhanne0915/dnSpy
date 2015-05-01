﻿/*
    Copyright (C) 2014-2015 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using dnlib.PE;
using dnlib.DotNet;

namespace ICSharpCode.ILSpy.AsmEditor.Method
{
	sealed class MethodDefOptions
	{
		public MethodImplAttributes ImplAttributes;
		public MethodAttributes Attributes;
		public UTF8String Name;
		public MethodSig MethodSig;
		public ImplMap ImplMap;

		public MethodDefOptions()
		{
		}

		public MethodDefOptions(MethodDef method)
		{
			this.ImplAttributes = method.ImplAttributes;
			this.Attributes = method.Attributes;
			this.Name = method.Name;
			this.MethodSig = method.MethodSig;
			this.ImplMap = method.ImplMap;
		}

		public MethodDef CopyTo(MethodDef method)
		{
			method.ImplAttributes = this.ImplAttributes;
			method.Attributes = this.Attributes;
			method.Name = this.Name;
			method.MethodSig = this.MethodSig;
			method.ImplMap = this.ImplMap;
			return method;
		}

		public MethodDef CreateMethodDef()
		{
			return new MethodDefUser(Name, MethodSig, ImplAttributes, Attributes) {
				ImplMap = ImplMap,
			};
		}

		public static MethodDefOptions Create(UTF8String name, MethodSig methodSig)
		{
			return new MethodDefOptions {
				ImplAttributes = MethodImplAttributes.IL | MethodImplAttributes.Managed,
				Attributes = MethodAttributes.Public | MethodAttributes.ReuseSlot | (methodSig.HasThis ? 0 : MethodAttributes.Static),
				Name = name,
				MethodSig = methodSig,
				ImplMap = null,
			};
		}
	}
}