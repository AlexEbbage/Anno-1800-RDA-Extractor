using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace kskudlik
{
	class OpenFolderBox
	{
		static string progver = "1.0";

		[STAThread]
		static int Main( string[] args )
		{
			string startfolder = Directory.GetCurrentDirectory( );
			string description = String.Format( "ofob,  Version {0}", progver );
			bool startfolderset = false;
			bool descriptionset = false;
			bool allowmakedir = false;

			#region Command Line Parsing

			foreach ( string arg in args )
			{
				if ( arg == "/?" )
				{
					return ShowHelp( );
				}
			}
			if ( args.Length > 3 )
			{
				return ShowHelp( "Too many command line arguments" );
			}
			if ( args.Length > 0 )
			{
				foreach ( string arg in args )
				{
					switch ( arg.ToUpper( ) )
					{
						case "/?":
							return ShowHelp( );
						case "/MD":
							if ( allowmakedir )
							{
								return ShowHelp( "Duplicate command line switch /MD" );
							}
							allowmakedir = true;
							break;
						default:
							if ( startfolderset )
							{
								if ( descriptionset )
								{
									return ShowHelp( "Invalid or duplicate command line argument \"{0}\"", arg );
								}
								description = arg.Replace( "\\n", "\n" ).Replace( "\\t", "\t" );
								descriptionset = true;
							}
							else
							{
								try
								{
									startfolder = Path.GetFullPath( arg );
								}
								catch ( ArgumentException )
								{
									startfolder = arg.Substring( 0, arg.IndexOf( '"' ) );
									startfolder = Path.GetFullPath( startfolder + "." );
								}
								if ( !Directory.Exists( startfolder ) )
								{
									return ShowHelp( "Invalid folder \"{0}\"", startfolder );
								}
								startfolderset = true;
							}
							break;
					}
				}
			}

			#endregion Command Line Parsing

			using ( FolderBrowserDialog dialog = new FolderBrowserDialog( ) )
			{
				dialog.SelectedPath = startfolder;
				dialog.Description = description;
				dialog.ShowNewFolderButton = allowmakedir;
				if ( dialog.ShowDialog( ) == DialogResult.OK )
				{
					Console.WriteLine( dialog.SelectedPath );
					return 0;
				}
				else
				{

					return 2;
				}
			}
		}


}
