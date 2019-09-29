ECHO D | XCOPY "Compact Control\bin\Release" "Release-%DATE%" /EXCLUDE:exclude.txt

IF EXIST Release-%DATE%.zip DEL Release-%DATE%.zip