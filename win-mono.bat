echo OFF
CALL test-mono.bat test-opentk			> results-mono-opentk.txt
CALL test-mono.bat test-opentk-sdl2 		> results-mono-opentk-sdl2.txt
CALL test-mono.bat test-sdl2cs 			> results-mono-sdl2cs.txt
CALL test-mono.bat test-sdl2cs-opentk-external 	> results-mono-sdl2cs-opentk.txt
CALL test-mono.bat test-sdl2-opentk-external	> results-mono-sdl2-opentk.txt
