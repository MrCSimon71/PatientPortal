const requireModule = (require as any).context('.', false, /\.store\.ts$/);
const modules: { [key: string]: any } = {};

requireModule.keys().forEach((filename: string) => {
  const moduleName = filename
    .replace(/(\.\/|\.store\.ts)/g, '')
    .replace(/^\w/, (c: string) => c.toUpperCase());

  modules[moduleName] = requireModule(filename).default || requireModule(filename);
});

export default modules;