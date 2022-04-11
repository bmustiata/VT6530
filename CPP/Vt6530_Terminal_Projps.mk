
Vt6530_Terminal_Projps.dll: dlldata.obj Vt6530_Terminal_Proj_p.obj Vt6530_Terminal_Proj_i.obj
	link /dll /out:Vt6530_Terminal_Projps.dll /def:Vt6530_Terminal_Projps.def /entry:DllMain dlldata.obj Vt6530_Terminal_Proj_p.obj Vt6530_Terminal_Proj_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del Vt6530_Terminal_Projps.dll
	@del Vt6530_Terminal_Projps.lib
	@del Vt6530_Terminal_Projps.exp
	@del dlldata.obj
	@del Vt6530_Terminal_Proj_p.obj
	@del Vt6530_Terminal_Proj_i.obj
