# PdfUtil
Open Source Pdf Utility

Prerequisite : 
Need .Net 5 Desktop Runtime or Hosting Bundle to be installed on the target machine.
It can be downloaded from microsoft site -> https://dotnet.microsoft.com/en-us/download/dotnet/5.0

Usage : 
  
  Open command prompt from the location where this utility is placed then execute refering the following example.
  
  Syntax: PdfUtil.exe -f [Pdf File Path] -d [Output Directory] --directory [Directory for Files] -o [Operation Unlock|Protect] -l [Launch file] -p [Password]
  
  Ex : PdfUtil.exe -f "c:\Documents\Test.pdf" -d "c:\Output" --directory "c:\Documents" -o Unlock -l true -p 123
  
  -f or --filepath  : Pdf file path. Multiple files can be passed by adding comma (,) c:\Test.pdf,c:\Test1.pdf.<br />
  --directory       : Directory that has pdf files. -f or --directory can be used. if both passed --directory will be considered.Either one is mandatory.<br />
  -d or --output    : Destination folder where file will be placed once processed. Optional<br />
  -o or --operation : Pdf operation. Now only Unlock and Protect is available.<br />
  -l or --launch    : File will opened in the default pdf viewer if user pass true in CLI. Mandatory argument.<br />
  -p or --password  : To protect or unlock file this is needed. Mandatory in case of protect and unlock operation.<br />
