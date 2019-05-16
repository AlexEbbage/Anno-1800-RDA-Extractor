@echo off
setlocal
"rbb.exe" "Exportieren";"Importieren" "Bitte auswaehlen, ob die RDA exportiert oder zurueruck in die RDA importiert werden soll" "Anno 1800 RDA modifizieren" /W:500 > "tempDO"
set /p do=<"tempDO"
del "tempDO"

IF  "%do%" == "Exportieren" (
	cls
	echo Bitte die zu exportierende RDA-Datei auswaehlen.
	:selection_rda
	"ofb.exe" "Anno 1800 RDA Dateien (*.rda|*.rda" "%USERPROFILE%" "Bitte die zu exportierende RDA-Datei auswaehlen."   > "tempRDA"
	set /p rda=<"tempRDA"
	del "tempRDA"
	IF "%rda%" == "" (
		cls
		echo Keine gueltige RDA-Datei ausgewaehlt. Bitte erneut versuchen.
		goto selection_rda
	)
	cls
	echo Bitte den Ordner in den die RDA-Datei exportiert werden soll auswaehlen.
	:selection_directory
	"ofob.exe" "%USERPROFILE%" "Bitte den Ordner in den die RDA-Datei exportiert werden soll auswaehlen." /MD   > "tempDIR"
	set /p directory=<"tempDIR"
	del "tempDIR"
	IF "%directory%" == "" (
		cls
		echo Keine gueltiges Verzeichnis zum Export ausgewaehlt. Bitte erneut versuchen.
		goto selection_directory
	)
	cls
	echo Dateien werden nun mit quickbms exportiert...
	"quickbms.exe" "anno7.bms" %rda% %directory%

)
IF  "%do%" == "Importieren" (
	echo Bitte die RDA-Datei auswaehlen, in die die Dateien reimportiert werden sollen.
	:selection_rda_imp
	"ofb.exe" "Anno 1800 RDA Dateien (*.rda|*.rda" "%USERPROFILE%" "Bitte die RDA-Datei auswaehlen, in die die Dateien reimportiert werden sollen."   > "tempRDAimp"
	set /p rdaimp=<"tempRDAimp"
	del "tempRDAimp"
	IF "%rdaimp%" == "" (
		cls
		echo Keine gueltige RDA-Datei ausgewaehlt. Bitte erneut versuchen.
		goto selection_rda_imp
	)
	cls
	echo Bitte den Ordner, in dem die bearbeiteten Dateien liegen auswaehlen.
    echo Nur die zu veraendernden Daten im Ordner belassen und auf beibehalten der Ordnerstruktur der RDA-Datei achten!
	:selection_directory_imp
	"ofob.exe" "%USERPROFILE%" "Bitte den Ordner, in dem die bearbeiteten Dateien liegen auswaehlen." /MD   > "tempDIRimp"
	set /p directoryimp=<"tempDIRimp"
	del "tempDIRimp"
	IF "%directoryimp%" == "" (
		cls
		echo Keine gueltiges Verzeichnis ausgewaehlt. Bitte erneut versuchen.
		goto selection_directory_imp
	)
	cls
	echo Dateien werden nun mit quickbms reimportiert...
	quickbms.exe -G -w -r anno7.bms %rdaimp% %directoryimp%  
)
