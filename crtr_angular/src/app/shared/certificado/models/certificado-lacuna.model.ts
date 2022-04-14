export class CertificadoLacuna {
  canRemove: boolean;
  email: string;
  isRemote: boolean;
  issuerName: string;
  keyUsage: KeyUsageLacuna;
  pkiBrazil: PkiBrazilLacuna
  pkiItaly: PkiItaliaLacuna;
  subjectName: string;
  thumbprint: string;
  validityEnd: Date;
  validityStart: Date;
}


export class KeyUsageLacuna {
  crlSign: boolean;
  dataEncipherment: boolean;
  decipherOnly: boolean;
  digitalSignature: boolean;
  encipherOnly: boolean;
  keyAgreement: boolean;
  keyCertSign: boolean;
  keyEncipherment: boolean;
  nonRepudiation: boolean;
}

export class PkiBrazilLacuna {
  certificateType: string;
  cnpj: string;
  companyName: string;
  cpf: string;
  dateOfBirth: string;
  isAplicacao: boolean;
  isPessoaFisica: boolean;
  isPessoaJuridica: boolean;
  nis: string;
  oabNumero: string;
  oabUF: string;
  responsavel: string;
  rgEmissor: string;
  rgEmissorUF: string;
  rgNumero: string;
}

export class PkiItaliaLacuna {
  codiceFiscale: string;
}
