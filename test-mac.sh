#!/bin/sh
monolinker -a $1/bin/Release/$1.exe -l none -out $1/bin/Release/min/
for i in {1..5}
do
 echo ===============
 echo $i
 rm $1.mlpd
 LD_LIBRARY_PATH=/Library/Frameworks/Mono.framework/Versions/Current/lib mono --profile=log:alloc,nocalls,output=$1.mlpd $1/bin/Release/$1.exe
 ls -l $1/bin/Release/OpenTK.dll
 ls -l $1/bin/Release/SDL2-CS.dll
done
echo ===============
echo ===============
echo ===============
for i in {1..5}
do
 echo ===============
 echo $i
 rm $1-min.mlpd
 LD_LIBRARY_PATH=/Library/Frameworks/Mono.framework/Versions/Current/lib mono --profile=log:alloc,nocalls,output=$1-min.mlpd $1/bin/Release/min/$1.exe
 ls -l $1/bin/Release/min/OpenTK.dll
 ls -l $1/bin/Release/min/SDL2-CS.dll
done

