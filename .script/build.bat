@echo off
cd ..
go build -gcflags="all=-trimpath=$GOPATH -l" -asmflags=-trimpath=$GOPATH -ldflags="-w -s" .
pause
