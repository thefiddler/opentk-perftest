lib\managed\monolinker -a %1\bin\Release\%1.exe -l none -out %1\bin\Release\min\
xcopy /y lib\x86\SDL2.dll %1\bin\Release\
xcopy /y lib\x86\SDL2.dll %1\bin\Release\min\

for %%i in (1,2,3,4,5) do (
 echo ===============
 echo %%i
 del %1.mlpd
 mono --profile=log:alloc,nocalls,output=%1.mlpd %1\bin\Release\%1.exe
 dir %1\bin\Release\OpenTK.dll
 dir %1\bin\Release\SDL2-CS.dll
)
echo ===============
echo ===============
echo ===============
for %%i in (1,2,3,4,5) do (
 echo ===============
 echo %%i
 del %1-min.mlpd
 mono --profile=log:alloc,nocalls,output=%1-min.mlpd %1\bin\Release\min\%1.exe
 dir %1\bin\Release\min\OpenTK.dll
 dir %1\bin\Release\min\SDL2-CS.dll
)
