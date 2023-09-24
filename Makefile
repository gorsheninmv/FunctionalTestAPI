fmt:
	find . -type f -name "*.fs" -not -path "*obj*" | xargs dotnet fantomas
