using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace kskudlik
{
	class OpenFile
	{
		static string progver = "1.0";

		[STAThread]
		static int Main( string[] args )
		{
			foreach ( string arg in args )
			{
				if ( arg == "/?" )
				{
					return ShowHelp( );
				}
			}

			using ( OpenFileDialog dialog = new OpenFileDialog( ) )
			{
				string filter = "All files (*.*)|*.*";
				string folder = Directory.GetCurrentDirectory( );
				string title = String.Format( "ofb", progver );

				if ( args.Length > 3 )
				{
					return ShowHelp( "Too many command line arguments" );
				}
				if ( args.Length > 0 )
				{
					filter = args[0];

					if ( Regex.IsMatch( filter, @"^\*\.(\*|\w+)$" ) )
					{
						string ext = filter.Substring( 2 ).ToLower( );
						if ( ext == ".*" )
						{
							filter = String.Format( "All files (*.{0})|*.{0}", ext );
						}
						else
						{
							filter = String.Format( "{0} files (*.{0})|*.{0}", ext );
						}
					}

					if ( !Regex.IsMatch( filter, @"All files\s+\(\*\.\*\)\|\*\.\*", RegexOptions.IgnoreCase ) )
					{
						if ( String.IsNullOrWhiteSpace( filter ) )
						{
							filter = "All files (*.*)|*.*";
						}
						else
						{
							filter = filter + "|All files (*.*)|*.*";
						}
					}

					if ( args.Length > 1 )
					{
						try
						{
							folder = Path.GetFullPath( args[1] );
						}
						catch ( ArgumentException )
						{

							folder = args[1].Substring( 0, args[1].IndexOf( '"' ) );
							folder = Path.GetFullPath( folder + "." );
						}
						if ( !Directory.Exists( folder ) )
						{
							return ShowHelp( "Invalid folder \"{0}\"", folder );
						}

						if ( args.Length > 2 )
						{
							title = args[2];
						}
					}
				}
				dialog.Filter = filter;
				dialog.FilterIndex = 1;
				dialog.InitialDirectory = folder;
				dialog.Title = title;
				dialog.RestoreDirectory = true;
				if ( dialog.ShowDialog( ) == DialogResult.OK )
				{
					Console.WriteLine( dialog.FileName );
					return 0;
				}
				else
				{
					return 2;
				}
			}
		}


}
