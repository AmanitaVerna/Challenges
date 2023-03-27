using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Challenges {
	public static class ChallengeSeven {
		// The Challenge:
		// "Write a program that takes in a path to a .NET assembly file and writes out to a file with the same name but .TXT..
		// In this file will be a tree of the namespaces inside of it, with their classes/structs, and every member of those classes/structs
		// (nicely, e.g., indent them so each layer down is further over perhaps). Hint: this is a reflection challenge."
		public static void Run() {
			Console.WriteLine("Please drag a .NET assembly file onto the console window and then press enter: ");
			string? filename = null;
			while (filename == null) {
				filename = Console.ReadLine();
			}
			if (filename.Length == 0) {
				return;
			}
			filename = filename.Trim('"');
			// see if there's a file with this name, and see if we can load it
			Assembly? assembly = null;
			try {
				assembly = Assembly.LoadFile(filename);
			} catch (Exception e) {
				Console.WriteLine("Failed to load assembly from file "+filename+": "+e.Message);
			}
			if (assembly != null) {
				// remove extension, replace with .txt
				int dot = filename.LastIndexOf('.');
				string outExt = ".txt";
				if (dot != -1) {
					filename = filename.Substring(0, dot) + outExt;
				} else {
					filename += outExt;
				}
				// create text file
				using (var w = File.Create(filename)) {
					using (StreamWriter sw = new StreamWriter(w)) {
						// locate all namespaces
						var namespaces = assembly.GetTypes().Select(t => t.Namespace).Distinct();
						// for each, print "namespace <name> {"
						foreach (string? n in namespaces) {
							if (n != null) {
								sw.WriteLine("namespace " + n + " {");
								// locate all classes and structs in each namespace
								var types = assembly.GetTypes().Where(t => t.Namespace == n);
								// for each, print "<class|struct> <name> {", tabbed over once
								foreach (Type? t in types) {
									bool classOrStruct = false;
									if (t != null && t.IsTypeDefinition) {
										if (t.IsClass) {
											sw.WriteLine("\tclass " + t.Name + " {");
											classOrStruct = true;
										} else if (t.IsValueType && !t.IsEnum) {
											sw.WriteLine("\tstruct " + t.Name + " {");
											classOrStruct = true;
										}
									}
									if (t != null && classOrStruct) { // I know t is guaranteed to be null if classOrStruct is true, but the compiler doesn't seem to
										// for each, print their members, tabbed over twice, ending lines with ";"
										var members = t.GetMembers();
										foreach (MemberInfo m in members) {
											if (m is FieldInfo) {
												FieldInfo fi = (FieldInfo)m;
												sw.WriteLine("\t\t"+fi.FieldType.Name+" "+fi.Name+";");
											} else if (m is PropertyInfo) {
												PropertyInfo pi = (PropertyInfo)m;
												MethodInfo[] accessors = pi.GetAccessors();
												string accStr = "";
												foreach (MethodInfo accessor in accessors) {
													if (accessor.GetParameters().Length == 0) {
														accStr += " get;";
													} else {
														accStr += " set;";
													}
												}
												sw.WriteLine("\t\t" + pi.PropertyType.Name + " " + pi.Name + accStr);
											} else if (m is MethodInfo) {
												MethodInfo mi = (MethodInfo)m;
												string parStr = "";
												foreach (ParameterInfo pi in mi.GetParameters()) {
													if (parStr.Length > 0) {
														parStr += ", ";
													}
													parStr += pi.ParameterType+" "+pi.Name;
												}
												sw.WriteLine("\t\t" + mi.ReturnType.Name + " " + mi.Name + "(" + parStr + ");");
											} else {
												sw.WriteLine("\t\t" + m.MemberType + " " + m.Name);
											}			
										}
										// print "\t}"s closing each class and struct
										sw.WriteLine("\t}");
									}
								}
								// print "}"s closing namespaces
								sw.WriteLine("}");
							}
						}
						// display message saying that the text file was written.
						Console.WriteLine("Wrote " + filename);
						// flush and close text file (closing the using block does this)
					}
				}
			}
		}
	}
}
