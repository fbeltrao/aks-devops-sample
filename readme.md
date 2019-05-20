# Sample AKS application using DevSpaces



## Installing app manually

```bash
cd charts/
helm install --debug -n myappcomposite . --dep-up
```

## Updating a dependency

```bash
cd charts/
helm dependency update .
```