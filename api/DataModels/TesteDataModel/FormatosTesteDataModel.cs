using BitHelp.Core.Type.pt_BR;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.TesteDataModel {
    public class FormatosTesteDataModel
        : Common.BaseDataModel<FormatosTesteDataModel> {

        private string _string;
        public string String {
            get => _string;
            set {
                _string = value;
                RegistrarPropriedade();
            }
        }

        private IntInput _int;
        public IntInput Int {
            get => _int;
            set {
                _int = value;
                this.RemoveAtReference(x => x.Int);
                this.InputTypeIsValid(x => x.Int);
                RegistrarPropriedade();
            }
        }

        private LongInput _long;
        public LongInput Long {
            get => _long;
            set {
                _long = value;
                this.RemoveAtReference(x => x.Long);
                this.InputTypeIsValid(x => x.Long);
                RegistrarPropriedade();
            }
        }

        private DecimalInput _decimal;
        public DecimalInput Decimal {
            get => _decimal;
            set {
                _decimal = value;
                this.RemoveAtReference(x => x.Decimal);
                this.InputTypeIsValid(x => x.Decimal);
                RegistrarPropriedade();
            }
        }

        private DoubleInput _double;
        public DoubleInput Double {
            get => _double;
            set {
                _double = value;
                this.RemoveAtReference(x => x.Double);
                this.InputTypeIsValid(x => x.Double);
                RegistrarPropriedade();
            }
        }

        private FloatInput _float;
        public FloatInput Float {
            get => _float;
            set {
                _float = value;
                this.RemoveAtReference(x => x.Float);
                this.InputTypeIsValid(x => x.Float);
                RegistrarPropriedade();
            }
        }

        private BoolInput _bool;
        public BoolInput Bool {
            get => _bool;
            set {
                _bool = value;
                this.RemoveAtReference(x => x.Bool);
                this.InputTypeIsValid(x => x.Bool);
                RegistrarPropriedade();
            }
        }

        private DateTimeInput _dateTime;
        public DateTimeInput DateTime {
            get => _dateTime;
            set {
                _dateTime = value;
                this.RemoveAtReference(x => x.DateTime);
                this.InputTypeIsValid(x => x.DateTime);
                RegistrarPropriedade();
            }
        }

        private TimeSpanInput _timeSpan;
        public TimeSpanInput TimeSpan {
            get => _timeSpan;
            set {
                _timeSpan = value;
                this.RemoveAtReference(x => x.TimeSpan);
                this.InputTypeIsValid(x => x.TimeSpan);
                RegistrarPropriedade();
            }
        }

        private GuidInput _guid;
        public GuidInput Guid {
            get => _guid;
            set {
                _guid = value;
                this.RemoveAtReference(x => x.Guid);
                this.InputTypeIsValid(x => x.Guid);
                RegistrarPropriedade();
            }
        }

        private EnumInput<Status> _enum;
        public EnumInput<Status> Enum {
            get => _enum;
            set {
                _enum = value;
                this.RemoveAtReference(x => x.Enum);
                this.InputTypeIsValid(x => x.Enum);
                RegistrarPropriedade();
            }
        }

        private PhoneType? _phone;
        public PhoneType? Phone {
            get => _phone;
            set {
                _phone = value;
                this.RemoveAtReference(x => x.Phone);
                this.InputTypeIsValid(x => x.Phone);
                RegistrarPropriedade();
            }
        }

        public FormatosTesteCmd Montar() {

            var resultado = new FormatosTesteCmd();

            if (PropriedadeRegistrada(x => x.String)) {
                resultado.String = String;
            }

            if (PropriedadeRegistrada(x => x.Int)) {
                if (!this.HasNotification(x => x.Int)) {
                    resultado.Int = (int?)Int;
                }
            }

            if (PropriedadeRegistrada(x => x.Long)) {
                if (!this.HasNotification(x => x.Long)) {
                    resultado.Long = (long?)Long;
                }
            }

            if (PropriedadeRegistrada(x => x.Decimal)) {
                if (!this.HasNotification(x => x.Decimal)) {
                    resultado.Decimal = (decimal?)Decimal;
                }
            }

            if (PropriedadeRegistrada(x => x.Double)) {
                if (!this.HasNotification(x => x.Double)) {
                    resultado.Double = (double?)Double;
                }
            }

            if (PropriedadeRegistrada(x => x.Float)) {
                if (!this.HasNotification(x => x.Float)) {
                    resultado.Float = (float?)Float;
                }
            }

            if (PropriedadeRegistrada(x => x.Bool)) {
                if (!this.HasNotification(x => x.Bool)) {
                    resultado.Bool = (bool?)Bool;
                }
            }

            if (PropriedadeRegistrada(x => x.DateTime)) {
                if (!this.HasNotification(x => x.DateTime)) {
                    resultado.DateTime = (DateTime?)DateTime;
                }
            }

            if (PropriedadeRegistrada(x => x.TimeSpan)) {
                if (!this.HasNotification(x => x.TimeSpan)) {
                    resultado.TimeSpan = (TimeSpan?)TimeSpan;
                }
            }

            if (PropriedadeRegistrada(x => x.Guid)) {
                if (!this.HasNotification(x => x.Guid)) {
                    resultado.Guid = (Guid?)Guid;
                }
            }

            if (PropriedadeRegistrada(x => x.Enum)) {
                if (!this.HasNotification(x => x.Enum)) {
                    resultado.Enum = (Status?)Enum;
                }
            }

            if (PropriedadeRegistrada(x => x.Phone)) {
                if (!this.HasNotification(x => x.Phone)) {
                    resultado.Phone = Phone;
                }
            }

            resultado.AddNotifications(this);

            return resultado;
        }

        public override bool IsValid() {
            return _notifications.IsValid();
        }
    }
}
