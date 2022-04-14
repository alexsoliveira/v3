
export interface WebPkiConfig {
    license: string;
    endpoint: string;
  }
  
  export interface ConfigurationModel {
    webPki: WebPkiConfig;    
  }
  
  export class Configuration {
  
    private _value: ConfigurationModel;
  
    initialize(model: ConfigurationModel) {
      this._value = model;
    }
  
    get value(): ConfigurationModel {
      if (!this._value) {
        throw 'The configuration has not been initialized';
      }
      return this._value;
    }
  }
  
  export const Config: Configuration = new Configuration();
  