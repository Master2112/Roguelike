@ECHO off
echo "Marking empty directories so Git won't complain..."
util\MarkEmptyDirs.exe -v Source/
ping 1.1.1.1 -n 1 -w 3000 > nul