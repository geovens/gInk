CRCCheck On
ShowInstDetails show
RequestExecutionLevel admin
!define APP_NAME "gInk"
!define DESCRIPTION "gInk"
!define LICENSE_TXT "license.txt"
!define INSTALLER_NAME "release.exe"
!define MAIN_APP_EXE "gInk.exe"
!define INSTALL_TYPE "SetShellVarContext all"
!define REG_ROOT "HKLM"
!define REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\${MAIN_APP_EXE}"
!define UNINSTALL_PATH "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"
######################################################################

VIProductVersion  "${VERSION}"
VIAddVersionKey "ProductName"  "${APP_NAME}"
VIAddVersionKey "CompanyName"  "${COMP_NAME}"
VIAddVersionKey "LegalCopyright"  "${COPYRIGHT}"
VIAddVersionKey "FileDescription"  "${DESCRIPTION}"
VIAddVersionKey "FileVersion"  "${VERSION}"

######################################################################
SetCompressor ZLIB
Name "${APP_NAME}"
Caption "${APP_NAME}"
OutFile "${INSTALLER_NAME}"
BrandingText "${APP_NAME}"
XPStyle on
InstallDirRegKey "${REG_ROOT}" "${REG_APP_PATH}" ""
InstallDir "$PROGRAMFILES\gInk"
!include "MUI.nsh"
!include "x64.nsh"
!define MUI_ABORTWARNING
!define MUI_UNABORTWARNING
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "${LICENSE_TXT}"
!insertmacro MUI_PAGE_DIRECTORY

!ifdef REG_START_MENU
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "gInk"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${REG_ROOT}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${UNINSTALL_PATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${REG_START_MENU}"
!insertmacro MUI_PAGE_STARTMENU Application $SM_Folder
!endif
!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_RUN
!define MUI_FINISHPAGE_RUN_FUNCTION "doRun"
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH
Section -MainProgram
	SetOutPath "$INSTDIR"
		File "bin/config_default.ini"
		File "bin/config.ini"
		File "bin/gInk.exe"
		File "bin/gInk.exe.config"
		File "bin/gInk.pdb"
		File "bin/hotkeys.ini"
		File "bin/icon_red.ico"
		File "bin/icon_white.ico"
		File "bin/Microsoft.Ink.dll"
		File "bin/pens.ini"
	SetOutPath "$INSTDIR\lang"
		File"bin/lang/ar.txt"
		File"bin/lang/as.txt"
		File"bin/lang/bn-bd.txt"
		File"bin/lang/cz.txt"
		File"bin/lang/de.txt"
		File"bin/lang/en-us.txt"
		File"bin/lang/es-latam.txt"
		File"bin/lang/fa.txt"
		File"bin/lang/fr.txt"
		File"bin/lang/hd.txt"
		File"bin/lang/he.txt"
		File"bin/lang/hr.txt"
		File"bin/lang/hu.txt"
		File"bin/lang/id-in.txt"
		File"bin/lang/it.txt"
		File"bin/lang/ja-jp.txt"
		File"bin/lang/kn.txt"
		File"bin/lang/ko-kr.txt"
		File"bin/lang/lt.txt"
		File"bin/lang/pl.txt"
		File"bin/lang/pt-br.txt"
		File"bin/lang/pt.txt"
		File"bin/lang/ro.txt"
		File"bin/lang/ru-ru.txt"
		File"bin/lang/sk.txt"
		File"bin/lang/th.txt"
		File"bin/lang/tr.txt"
		File"bin/lang/zh-cn.txt"
		File"bin/lang/zh-tw.txt"
SectionEnd
Section -Icons_Reg
SetOutPath "$INSTDIR"
WriteUninstaller "$INSTDIR\uninstall.exe"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
CreateDirectory "$SMPROGRAMS\$SM_Folder"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk" "$INSTDIR\$MAIN_APP_EXE"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\$MAIN_APP_EXE"
CreateShortCut "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!insertmacro MUI_STARTMENU_WRITE_END
!endif

!ifndef REG_START_MENU
CreateDirectory "$SMPROGRAMS\gInk"
CreateShortCut "$SMPROGRAMS\gInk\${APP_NAME}.lnk" "$INSTDIR\$MAIN_APP_EXE"
CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\$MAIN_APP_EXE"
CreateShortCut "$SMPROGRAMS\gInk\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"
!endif

WriteRegStr ${REG_ROOT} "$REG_APP_PATH" "" "$INSTDIR\$MAIN_APP_EXE"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayName" "${APP_NAME}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "UninstallString" "$INSTDIR\uninstall.exe"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayIcon" "$INSTDIR\$MAIN_APP_EXE"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayVersion" "${VERSION}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "Publisher" "${COMP_NAME}"

!ifdef WEB_SITE
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "URLInfoAbout" "${WEB_SITE}"
!endif
SectionEnd

Section Uninstall
Delete "$INSTDIR\AbecedarV2.exe"

RmDir "$INSTDIR"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_GETFOLDER "Application" $SM_Folder
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk"
!endif
Delete "$DESKTOP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\$SM_Folder"
!endif

!ifndef REG_START_MENU
Delete "$SMPROGRAMS\gInk\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\gInk\Uninstall ${APP_NAME}.lnk"
Delete "$DESKTOP\${APP_NAME}.lnk"

RmDir "$SMPROGRAMS\gInk"
!endif

DeleteRegKey ${REG_ROOT} "$REG_APP_PATH"
DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd
Function un.onInit
	!insertmacro MUI_UNGETLANGUAGE
	StrCpy $MAIN_APP_EXE "AbecedarV2.exe"
	${un.GetWindowsVersion} $R0
	StrCmp $R0 "XP" skipLauncher setLauncher
	setLauncher:
	StrCpy $MAIN_APP_EXE "AbecedarV2_Loader.exe"
	skipLauncher:
	StrCpy $REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\$MAIN_APP_EXE"
FunctionEnd
