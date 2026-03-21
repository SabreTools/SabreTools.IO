

Project to port ZLib from C to C# (CSharp).

Src zlib 1.2.12 2022-Mar-28 - https://github.com/madler/zlib

See the Stages folder

1_zlib.c            - Created by running 1_zlib.c_Concat.ps1 Builds with Clang (used by hebron to convert)
                    - Only deflate, inflate, crc32 and adler32 code at the moment. GZip might be added if required.
                    - The only edits to these files are to remove any #includes that have been combined
                    - The file list includes a 000_ to insert any #defines etc and 100_ for a main for debugging etc
                    - Notice crc32.c and trees.c had to be split to allow the single file to build

2_zlib.cs_Converted - The converted output that Hebron produced - https://github.com/HebronFramework/Hebron
                    - This is a little app that uses Clang to read the C code as DOM and write with Roslyn
                    - It does a fairly decent job and removes a lot of complication

3_zlib.cs_Working   - The fixed up and amended C# that actually runs and matches the C code output
                    - It's had minimal change so is not the prettiest C# code
                    - It's Unsafe in places
          
Deflate and Inflate streams have been added.
