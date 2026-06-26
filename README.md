# EPUBtoCBZ
A C# Command line tool to convert an EPUB file to a CBZ file

This project is possible because EPUBs & CBZs are ZIP files under the hood with only a slight difference ; their directories and file organization

# Usage
You can use the command in the following ways to convert EPUBs to CBZs

## Single File
The following command will create a new CBZ file of the same name from the original EPUB file
```bash
EPUBtoCBZ.exe <epub-file>.epub
```

If you'd like to rename the file you can use the following command :
```bash
EPUBtoCBZ.exe <epub-file>.epub <new-name>
```

## Mass Conversion
If you want to convert an entire folders worth of EPUBs to CBZ use the following command (NOTE : This script also deletes the EPUB afterwards if it succeeded conversion)
```bash
find . -type f -name "*.epub" -exec sh -c 'EPUBtoCBZ.exe "$1" "${1%.epub}.cbz" && rm "$1"' _ {} \;
```
