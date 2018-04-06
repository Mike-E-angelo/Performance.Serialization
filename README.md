# Performance.Serialization

Basic performance testing to delineate between direct serialization and dom-traversal/querying.

Sample times:
```
         Method |     Mean |     Error |    StdDev |
--------------- |---------:|----------:|----------:|
    Deserialize | 10.66 us | 0.2077 us | 0.2550 us |
 DeserializeDom | 13.77 us | 0.2688 us | 0.3769 us |
 ```