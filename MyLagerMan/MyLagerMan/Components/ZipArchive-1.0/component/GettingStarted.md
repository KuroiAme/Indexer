MiniZip.ZipArchive is a Xamarin.iOS binding based on the open source
Objective-C ZipArchive library.  It adds the ability to zip and unzip
files to your app.

For quick and simple usage, use the EasyZip and EasyUnZip methods.

## Examples

### Using EasyZip to zip files

```csharp
string personal = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
string zipFile = Path.Combine (personal, "MyFiles.zip");
string firstFile = Path.Combine (personal, "MyFile.txt");
string secondFile = Path.Combine (personal, "MyPhoto.jpg");

var zip = new ZipArchive ();
zip.EasyZip (zipFile, new [] { firstFile, secondFile }, "/", "");
```

### Using EasyUnZip to unzip files

```csharp
string personal = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
string target = Path.Combine (personal, "unzipped");
string zipFile = Path.Combine (personal, "MyFiles.zip");

var zip = new ZipArchive ();
zip.EasyUnzip (zipFile, target, true, "");
```

### Stepwise zip creation

```csharp
var zip = new ZipArchive ();
zip.CreateZipFile ("myfiles.zip", "passw0rd");
zip.AddFolder ("my_directory", "prefix");
zip.CloseZipFile ();
```

### Unzip a file:

```csharp
var zip = new ZipArchive ();
zip.UnzipOpenFile ("myfiles.zip", "passw0rd");
zip.UnzipFileTo ("my_directory1", true);

zip.OnError += (sender, args) => {
	Console.WriteLine ("Error while unzipping: {0}", args);
};

zip.UnzipCloseFile ();
```
