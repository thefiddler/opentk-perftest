echo OFF
CALL test-win.bat test-opentk			> results-win-opentk.txt
CALL test-win.bat test-opentk-sdl2 		> results-win-opentk-sdl2.txt
CALL test-win.bat test-sdl2cs 			> results-win-sdl2cs.txt
CALL test-win.bat test-sdl2cs-opentk-external 	> results-win-sdl2cs-opentk.txt
CALL test-win.bat test-sdl2-opentk-external	> results-win-sdl2-opentk.txt
