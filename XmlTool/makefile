all: msg.cs Program.cs Program2.cs
	@csc Program.cs
	@csc Program2.cs msg.cs

msg.xsd: msg.xml
	xsd msg.xml

msg.cs: msg.xsd
	xsd -c msg.xsd


clean:
	@del Program.exe Program2.exe msg.cs msg.xsd
